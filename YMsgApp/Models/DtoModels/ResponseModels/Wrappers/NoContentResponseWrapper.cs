namespace YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

public class NoContentResponseWrapper:ResponseWrapper<object>
{
    private new object ResponseObject=>null;

    public NoContentResponseWrapper() : base()
    {
        
    }
}