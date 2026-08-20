namespace OrderManagement.Application.Interface;

public interface IDataRepo
{
    Task SaveChangesAsync();
}