namespace BaseRateApp.Models.Response
{
    public interface IResponseModel<TKey>
    {
        TKey Id { get; set; }
    }
}
