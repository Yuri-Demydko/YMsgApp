namespace YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

public class EmptyResponseWrapper:ResponseWrapper<object>
{
    private new object ResponseObject => null;
}