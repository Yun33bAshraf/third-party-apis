using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;

namespace OpenProfileAPI.Application.Roles.Commands;
public class CreateRoleRightsCommand : IRequest<ResponseBase>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<CreateRoleRightsModel>? RoleRights { get; set; }
}

public class CreateRoleRightsModel
{
    public int RightId { get; set; }
}

public class CreateRoleRightsHandler : IRequestHandler<CreateRoleRightsCommand, ResponseBase>
{
    private readonly IDataRepository<Role> _roleRepository;
    private readonly IDataRepository<RoleRight> _roleRightRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUser _currentUser;

    public CreateRoleRightsHandler(IDataRepository<Role> roleRepository, IDataRepository<RoleRight> roleRightRepository, IUnitOfWork unitOfWork, IUser currentUser)
    {
        _roleRepository = roleRepository;
        _roleRightRepository = roleRightRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    private ResponseBase ErrorResponse(string error)
    {
        //_appLogger.Debug($"ErrorResponse Msg: {error}");
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }

    public async Task<ResponseBase> Handle(CreateRoleRightsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var role = new Role
            {
                Name = request.Name,
                Description = request.Description,
            };

            _roleRepository.Add(role, _currentUser.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.RoleRights != null)
            {
                foreach (var item in request.RoleRights)
                {
                    var roleRight = new RoleRight()
                    {
                        RightId = item.RightId,
                        RoleId = role.Id
                    };

                    _roleRightRepository.Add(roleRight, _currentUser.Id);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ResponseBase
            {
                Status = true,
                Message = AppMessage.RecordCreatedSuccessfully.GetDescription()
            };
        }
        catch (Exception ex)
        {
            //_appLogger.Error(ex);
            return ErrorResponse(ex.Message);
        }
    }
}
