namespace TheArmory.Domain.Models.Responce.Result.BaseResult;

public class SuccessResult : TheArmory.Domain.Models.Responce.Result.BaseResult.BaseResult
{
    public SuccessResult()
    {
        Success = true;
    }
}

public class SuccessResult<T> : BaseResult<T>
{
    public SuccessResult(T item)
    {
        Success = true;
        Item = item;
    }
}