using Ejercicio5Modulo3.Models;

public interface IUserService
{
    public Task<List<User>> GetUsersUsingFilter(String LastName, String Email);
    public Task LoadUsersInDB();

}