namespace OrderManagement.Application.Interface;

public interface IDataHelper
{
    public (string userId, string role) GetData();
}