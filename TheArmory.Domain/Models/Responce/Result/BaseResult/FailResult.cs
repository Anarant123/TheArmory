namespace TheArmory.Domain.Models.Responce.Result.BaseResult;

public class FailResult : TheArmory.Domain.Models.Responce.Result.BaseResult.BaseResult
{
    public FailResult()
    {
        Success = false;
        Error = "Unknown error";
    }

    public FailResult(string error)
    {
        Success = false;
        Error = error;
    }
}

