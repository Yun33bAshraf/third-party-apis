//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Common;
//using OpenProfileAPI.Domain.Entities;
//using OpenProfileAPI.Domain.Enums;

//namespace OpenProfileAPI.Application.Users.Commands.CreateAccount;

//public record CreateAccountCommand : IRequest<ResponseBase>
//{
//    public int UserId { get; set; }
//    public required string Password { get; set; }
//    public int RoleId { get; set; }
//}

//public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
//{
//    public CreateAccountCommandValidator()
//    {

//    }
//}

//public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ResponseBase>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IIdentityService _identityService;

//    public CreateAccountCommandHandler(IApplicationDbContext context, IIdentityService identityService)
//    {
//        _context = context;
//        _identityService = identityService;
//    }

//    private ResponseBase ErrorResponse(string error)
//    {
//        return new ResponseBase
//        {
//            Status = false,
//            Error = error
//        };
//    }

//    public async Task<ResponseBase> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
//    {
//        var isAccountCreated = await _identityService.CreateAccountPassword(request.UserId, request.Password);
//        if (!isAccountCreated)
//        {
//            return ErrorResponse(AppMessage.InvalidPassword.GetDescription());
//        }

//        var userRoles = _context.UserRole.Where(x => x.UserId == request.UserId);
//        foreach (var userRole in userRoles)
//        {
//            _context.UserRole.Remove(userRole);
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        // Create User Role Entry Here
//        var role = new UserRole();
//        role.UserId = request.UserId;
//        role.RoleId = request.RoleId;

//        _context.UserRole.Add(role);
//        await _context.SaveChangesAsync(cancellationToken);

//        return new ResponseBase()
//        {
//            Status = true,
//            Message = AppMessage.RecordCreatedSuccessfully.GetDescription()
//        };
//    }
//}
