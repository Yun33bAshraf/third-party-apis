using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using UserRoleModel = OpenProfileAPI.Domain.Entities.UserRole;

namespace OpenProfileAPI.Infrastructure.Data;
public class DataSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public DataSeeder(
        ApplicationDbContext dbContext,
        UserManager<User> userManager
    )
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public void Seed()
    {
        try
        {
            //SeedAuthPolicy();
            //SeedCategoriesAndEntityTypes();
            //SeedUsers();
            //SeedRoles();
            //SeedRights();
            //SeedRoleRights();
            //SeedUserRoles();
            //SeedSubscriptionPlans();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding failed: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private bool CheckTablesExist(string tableName)
    {
        if (!_dbContext.Database.CanConnect())
            return false;

        var connection = _dbContext.Database.GetDbConnection();
        var dbName = new MySqlConnectionStringBuilder(connection.ConnectionString).Database;

        var sqlCommand = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{dbName}' AND table_name = '{tableName}';";
        using (var command = connection.CreateCommand())
        {
            command.CommandText = sqlCommand;
            _dbContext.Database.OpenConnection();

            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }
    }

    #region USER

    private void SeedUsers()
    {
        if (!CheckTablesExist("user"))
        {
            Console.WriteLine("Users table does not exist yet. Skipping seeding...");
            return;
        }

        var usersToSeed = new List<(string Email, string FirstName, string LastName, UserType UserType)>
        {
            ("admin@iapply.com", "Admin", "iApply", UserType.Admin),
            ("customer1@iapply.com", "Customer", "One", UserType.Customer),
            ("customer2@iapply.com", "Customer", "Two", UserType.Customer),
        };

        foreach (var (email, firstName, lastName, userTypeId) in usersToSeed)
        {
            SeedUser(email, firstName, lastName, userTypeId);
        }
    }

    private void SeedUser(string email, string firstName, string lastName, UserType userTypeId)
    {
        if (!CheckTablesExist("userprofile"))
        {
            Console.WriteLine("User Profile table does not exist yet. Skipping seeding...");
            return;
        }

        var existingUser = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
        if (existingUser != null)
        {
            Console.WriteLine($"{email} already exists. Skipping seeding...");
            return;
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = email,
            PhoneNumber = "03XXXXXXXXX",
            UserTypeId = (int)userTypeId,
            AuthKey = TypeExtensions.GenerateRandomPassword(),
            SmsKey = TypeExtensions.GenerateRandomPassword(),
            EmailKey = TypeExtensions.GenerateRandomPassword(),
            TwoFactorEnabled = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true,
            LockoutEnabled = false,
            CreatedBy = 1,
            Created = DateTime.UtcNow,
            LastModifiedBy = 1,
            LastModified = DateTime.UtcNow,
        };

        var result = _userManager.CreateAsync(user, "Asdf@1234").GetAwaiter().GetResult();

        if (result.Succeeded)
        {
            Console.WriteLine($"{user.Email} seeded successfully.");

            // Re-fetch the created user to ensure user.Id is populated
            var createdUser = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            if (createdUser != null)
            {
                CreateUserProfileIfNotExists(createdUser, firstName, lastName, email);
            }
            else
            {
                Console.WriteLine($"Could not retrieve newly created user {email} to seed profile.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to seed {user.Email}:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.Code}: {error.Description}");
            }
        }
    }

    private void CreateUserProfileIfNotExists(User user, string firstName, string lastName, string email)
    {
        var userProfileExists = _dbContext.UserProfile.Any(up => up.UserId == user.Id);
        if (userProfileExists)
        {
            Console.WriteLine($"{email} profile already exists. Skipping seeding...");
            return;
        }

        var userProfile = new UserProfile
        {
            UserId = user.Id,
            DisplayName = $"{firstName} {lastName}",
            Address = "21st Jump Street",
            Email = email,
            MobileNumber = "03XXXXXXXXX",
            CityId = (int)City.Islamabad,
            StateId = (int)StateProvince.FederalCapital,
            CountryId = (int)Country.Pakistan,
            PostalCode = "44000",
            MaritalStatusId = (int)MaritalStatus.Single,
            GenderId = (int)Gender.Male,
            NationalId = "XXXXX-XXXXXXX-X",
            LinkedInProfile = "No LinkedIn Profile",
            UserBio = "No Bio",
            Occupation = "Software Engineer",
            DateOfBirth = new DateOnly(1990, 1, 1),
            CreatedBy = 1,
            Created = DateTime.UtcNow,
            LastModifiedBy = 1,
            LastModified = DateTime.UtcNow,
        };

        _dbContext.UserProfile.Add(userProfile);
        _dbContext.SaveChanges();
        Console.WriteLine($"{userProfile.DisplayName} profile seeded successfully.");
    }

    #endregion USER

    #region AUTH POLICY

    private void SeedAuthPolicy()
    {
        try
        {
            if (!CheckTablesExist("authpolicy"))
            {
                Console.WriteLine("AuthPolicies table does not exist. Skipping seeding...");
                return;
            }

            // Seed policies for each user type
            SeedPolicyIfNotExist(UserType.Admin);
            SeedPolicyIfNotExist(UserType.Customer);

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log exception for debugging or future analysis
            Console.WriteLine($"An error occurred while seeding AuthPolicies: {ex.Message}");
        }
    }

    private void SeedPolicyIfNotExist(UserType userType)
    {
        var existingPolicy = _dbContext.AuthPolicies.FirstOrDefault(ap => ap.UserTypeId == (int)userType);

        if (existingPolicy == null)
        {
            existingPolicy = new AuthPolicy
            {
                UserTypeId = (int)userType,
                CreatedBy = 1,  // Ideally should come from logged-in user or configuration
                Created = DateTime.UtcNow
            };

            _dbContext.AuthPolicies.Add(existingPolicy);
        }

        SetPolicyDefaults(existingPolicy);

        Console.WriteLine($"Policy for {userType} seeded successfully.");
    }

    private void SetPolicyDefaults(AuthPolicy policy)
    {
        policy.Enforce2FactorVerification = false;
        policy.EnforcePasswordChangeOnFirstLogin = false;
        policy.EnforceBackendActivation = false;
        policy.EnforceEmailConfirmation = false;
        policy.EnforceMobileConfirmation = false;
        policy.EnforceProfileCompletion = false;
        policy.BaseTokenDurationMinutes = 360;
        policy.FullTokenDurationMinutes = 1440;
        policy.RefreshTokenDurationMinutes = 2880;
    }

    #endregion AUTH POLICY

    #region ROLE, RIGHTS & USER ROLES/RIGHTS

    public void SeedRoles()
    {
        if (!CheckTablesExist("role"))
        {
            Console.WriteLine("Role table does not exist yet. Skipping seeding...");
            return;
        }

        var rolesToSeed = Enum.GetValues(typeof(RoleType))
                              .Cast<RoleType>()
                              .Select(role =>
                              {
                                  var attr = role.ToRoleAttribute();
                                  return new Role
                                  {
                                      Id = attr.Id,
                                      Name = attr.Name,
                                      Description = attr.Description
                                  };
                              })
                              .ToList();

        var dbSet = _dbContext.Set<Role>();
        var existingRoles = dbSet.ToList();

        foreach (var seedRole in rolesToSeed)
        {
            var existing = existingRoles.FirstOrDefault(r => r.Id == seedRole.Id);

            if (existing == null)
            {
                dbSet.Add(seedRole);
            }
            else
            {
                if (existing.Name != seedRole.Name ||
                    existing.Description != seedRole.Description)
                {
                    existing.Name = seedRole.Name;
                    existing.Description = seedRole.Description;
                }
            }
        }

        _dbContext.SaveChanges();
    }

    public void SeedRights()
    {
        if (!CheckTablesExist("right"))
        {
            Console.WriteLine("Right table does not exist yet. Skipping seeding...");
            return;
        }

        var rightsToSeed = Enum.GetValues(typeof(ApplicationRights))
                               .Cast<ApplicationRights>()
                               .Select(right =>
                               {
                                   var attr = right.ToRightAttribute();
                                   Console.WriteLine($"Processing right: {right}, Id: {attr.Id}, Name: {attr.Name}");
                                   return new Right
                                   {
                                       Id = attr.Id,
                                       Name = attr.Name,
                                       Description = attr.Name
                                   };
                               }).ToList();

        var dbSet = _dbContext.Set<Right>();
        var existingRights = dbSet.ToList();

        foreach (var seedRight in rightsToSeed)
        {
            var existing = existingRights.FirstOrDefault(r => r.Id == seedRight.Id);

            if (existing == null)
            {
                Console.WriteLine($"Adding new right: {seedRight.Name} ({seedRight.Id})");
                dbSet.Add(seedRight);
            }
            else
            {
                if (existing.Name != seedRight.Name ||
                    existing.Description != seedRight.Description)
                {
                    Console.WriteLine($"Updating right: {existing.Name} ({existing.Id})");
                    existing.Name = seedRight.Name;
                    existing.Description = seedRight.Description;
                }
            }
        }

        _dbContext.SaveChanges();
        Console.WriteLine("Seeding rights completed.");
    }

    public void SeedRoleRights()
    {
        if (!CheckTablesExist("roleright"))
        {
            Console.WriteLine("RoleRight table does not exist yet. Skipping seeding...");
            return;
        }

        var roleRightDbSet = _dbContext.Set<RoleRight>();
        var existingRoleRights = roleRightDbSet.ToList();

        var desiredRoleRights = Enum.GetValues(typeof(ApplicationRights))
            .Cast<ApplicationRights>()
            .SelectMany(right =>
            {
                var attr = right.ToRightAttribute();
                return attr.RoleIds.Select(roleId => new RoleRight
                {
                    RoleId = roleId,
                    RightId = attr.Id
                });
            })
            .ToList();

        var toAdd = desiredRoleRights
            .Where(rr => !existingRoleRights.Any(er => er.RoleId == rr.RoleId && er.RightId == rr.RightId))
            .ToList();

        var toRemove = existingRoleRights
            .Where(er => !desiredRoleRights.Any(rr => rr.RoleId == er.RoleId && rr.RightId == er.RightId))
            .ToList();

        if (toRemove.Count > 0)
            roleRightDbSet.RemoveRange(toRemove);

        if (toAdd.Count > 0)
            roleRightDbSet.AddRange(toAdd);

        if (toAdd.Count > 0 || toRemove.Count > 0)
            _dbContext.SaveChanges();

        Console.WriteLine($"Role Rights seeded successfully");
    }

    private void SeedUserRoles()
    {
        if (!CheckTablesExist("userrole"))
        {
            Console.WriteLine("UserRoles table does not exist yet. Skipping seeding...");
            return;
        }

        var usersToRoles = new List<(string Email, RoleType Role)>
        {
            ("admin@iapply.com", RoleType.Administration),
            ("customer1@iapply.com", RoleType.Customer),
            ("customer2@iapply.com", RoleType.Customer)
        };

        foreach (var (email, roleEnum) in usersToRoles)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user == null)
            {
                Console.WriteLine($"User with email {email} not found. Skipping role assignment...");
                continue;
            }

            var role = _dbContext.Role.FirstOrDefaultAsync(r => r.Id == (int)roleEnum).GetAwaiter().GetResult();
            if (role == null)
            {
                Console.WriteLine($"Role {roleEnum} not found. Skipping role assignment...");
                continue;
            }

            var existing = _dbContext.UserRole
                .Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id);

            if (existing)
            {
                Console.WriteLine($"{roleEnum} already assigned to {user.Email}. Skipping...");
                continue;
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            _dbContext.UserRole.Add(userRole);
            _dbContext.SaveChanges();

            Console.WriteLine($"Successfully assigned {roleEnum} role to {user.Email}");
        }
    }

    #endregion ROLE, RIGHTS & USER ROLES/RIGHTS

    #region ENTITYTYPE & CATEGORY

    private void SeedCategoriesAndEntityTypes()
    {
        try
        {
            if (!CheckTablesExist("category") || !CheckTablesExist("entitytype"))
            {
                Console.WriteLine("Required tables do not exist. Skipping category and entity type seeding...");
                return;
            }

            SeedEntityTypes();
            SeedCategories();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding Category and EntityTypes: {ex.Message}");
        }
    }

    private void SeedEntityTypes()
    {
        var entityTypes = Enum.GetValues(typeof(Domain.Enums.EntityType))
            .Cast<Domain.Enums.EntityType>()
            .Select(et => new Domain.Entities.EntityType
            {
                Id = (int)et,
                Name = et.ToReadableString(),
                IsActive = true,
                CreatedBy = 1,
                Created = DateTime.UtcNow,
            });

        foreach (var entityType in entityTypes)
        {
            var existing = _dbContext.EntityType.FirstOrDefault(e => e.Id == entityType.Id);

            if (existing == null)
            {
                _dbContext.EntityType.Add(entityType);
            }
            else
            {
                if (existing.Name != entityType.Name || !existing.IsActive)
                {
                    existing.Name = entityType.Name;
                    existing.IsActive = true;
                    existing.LastModifiedBy = 1;
                    existing.LastModified = DateTime.UtcNow;

                    _dbContext.EntityType.Update(existing);
                }
            }
        }

        _dbContext.SaveChanges();
    }

    private void SeedCategories()
    {
        var categories = Enum.GetValues(typeof(Domain.Enums.Category)).Cast<Domain.Enums.Category>();

        foreach (var category in categories)
        {
            var (id, name, value, entityTypeId, parentCategoryId, description) = category.ToCategoryAttribute();

            var existing = _dbContext.Category.FirstOrDefault(c => c.Id == value);

            if (existing == null)
            {
                _dbContext.Category.Add(new Domain.Entities.Category
                {
                    Id = value,
                    Name = name,
                    EntityTypeId = entityTypeId,
                    ParentCategoryId = parentCategoryId,
                    Description = description,
                    DisplayOrder = null,
                    ColorCode = null,
                    CreatedBy = 1,
                    Created = DateTime.UtcNow,
                });
            }
            else
            {
                bool needsUpdate =
                    existing.Name != name ||
                    existing.EntityTypeId != entityTypeId ||
                    existing.ParentCategoryId != parentCategoryId ||
                    existing.Description != description;

                if (needsUpdate)
                {
                    existing.Name = name;
                    existing.EntityTypeId = entityTypeId;
                    existing.ParentCategoryId = parentCategoryId;
                    existing.Description = description;
                    existing.LastModifiedBy = 1;
                    existing.LastModified = DateTime.UtcNow;

                    _dbContext.Category.Update(existing);
                }
            }
        }

        _dbContext.SaveChanges();
    }

    #endregion ENTITYTYPE & CATEGORY

    #region SUBSCRIPTION PLANS

    //private void SeedSubscriptionPlans()
    //{
    //    try
    //    {
    //        if (!CheckTablesExist("subscriptionplan"))
    //        {
    //            Console.WriteLine("Subscription Plan table does not exist. Skipping seeding...");
    //            return;
    //        }

    //        var basicPlanExists = _dbContext.SubscriptionPlan.Any(sp => sp.StripePriceId == "price_1RVRtbCmxbAFmaj2Z0Sb2dxH");
    //        var proPlanExists = _dbContext.SubscriptionPlan.Any(sp => sp.StripePriceId == "price_1RVRuECmxbAFmaj2JpGLGZZo");
    //        var premiumPlanExists = _dbContext.SubscriptionPlan.Any(sp => sp.StripePriceId == "price_1RVRv0CmxbAFmaj2rQa6azdL");

    //        if (!basicPlanExists)
    //        {
    //            var basicPlan = new SubscriptionPlan
    //            {
    //                Name = "Basic",
    //                StripePriceId = "price_1RVRtbCmxbAFmaj2Z0Sb2dxH",
    //                MonthlyPrice = 10.99m,
    //                MaxApplicationsPerMonth = 10,
    //                MaxApplicationsPerDay = 3,
    //                IsDefault = true,
    //                Comment = "Perfect for individuals just starting out."
    //            };
    //            _dbContext.SubscriptionPlan.Add(basicPlan);
    //            Console.WriteLine("Basic Subscription Plan seeded.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Basic Subscription Plan already exists. Skipping...");
    //        }

    //        if (!proPlanExists)
    //        {
    //            var proPlan = new SubscriptionPlan
    //            {
    //                Name = "Pro",
    //                StripePriceId = "price_1RVRuECmxbAFmaj2JpGLGZZo",
    //                MonthlyPrice = 29.99m,
    //                MaxApplicationsPerMonth = 50,
    //                MaxApplicationsPerDay = 5,
    //                Comment = "Great for active job seekers."
    //            };
    //            _dbContext.SubscriptionPlan.Add(proPlan);
    //            Console.WriteLine("Pro Subscription Plan seeded.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Pro Subscription Plan already exists. Skipping...");
    //        }

    //        if (!premiumPlanExists)
    //        {
    //            var premiumPlan = new SubscriptionPlan
    //            {
    //                Name = "Premium",
    //                StripePriceId = "price_1RVRv0CmxbAFmaj2rQa6azdL",
    //                MonthlyPrice = 59.99m,
    //                MaxApplicationsPerMonth = int.MaxValue,
    //                MaxApplicationsPerDay = int.MaxValue,
    //                Comment = "Unlimited applications with premium support."
    //            };
    //            _dbContext.SubscriptionPlan.Add(premiumPlan);
    //            Console.WriteLine("Premium Subscription Plan seeded.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Premium Subscription Plan already exists. Skipping...");
    //        }

    //        _dbContext.SaveChanges();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log exception for debugging or future analysis
    //        Console.WriteLine($"An error occurred while seeding Subscription Plans: {ex.Message}");
    //    }
    //}

    #endregion SUBSCRIPTION PLANS
}
