using System;

namespace IApply.Frontend.Models.User;

public class CreateUserResponse
{
    public string userId { get; set; }
    public string message { get; set; }
    public int errorCode { get; set; }
    public bool isSuccess { get; set; }

}
