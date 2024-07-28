using Ejercicio5Modulo3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class UserService : IUserService
{

    private Ejercicio5Modulo3Context context;
    private IConfiguration configuration;
    private IHttpClientFactory httpClientFactory;

    public UserService(Ejercicio5Modulo3Context context, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        this.context = context;
        this.configuration = configuration;
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<List<User>> GetUsersUsingFilter(String LastName, String Email)
    {
        if (LastName == null && Email == null) return await context.User.ToListAsync();
        else if(LastName != null)
        {
            return await context.User.Where(x => x.Apellido.ToLower().Equals(LastName.ToLower())).ToListAsync();
        }
        else if(Email != null)
        {
            return await context.User.Where(x => x.Email.ToLower().Equals(Email.ToLower())).ToListAsync();
        }
        else
        {
            return await context.User.Where(x => x.Apellido.ToLower().Equals(LastName.ToLower()) || x.Email.ToLower().Equals(Email.ToLower())).ToListAsync();
        }

    }

    public async Task LoadUsersInDB()
    {
        if (context.User.Count() > 0) throw new Exception("Ya existen datos cargados en la DB.");

        var client = httpClientFactory.CreateClient("postService");
        var result = await client.GetAsync($"?results={configuration.GetSection("SEED_RESULT_NUMBER").Value}");
        var content = await result.Content.ReadAsStringAsync();
        var resultDes = JsonSerializer.Deserialize<ResultsDTO>(content);
        if (resultDes != null)
        {
            foreach (Results dto in resultDes.results)
            {
                Console.WriteLine(dto.login.userName);
                Console.WriteLine(dto.name.first);
                var user = new User(
                    dto.name.first, dto.name.last, dto.dob.age, dto.gender, dto.email,
                    dto.login.userName, dto.login.password, dto.location.city, dto.location.state, dto.location.country);

                await context.AddAsync(user);
            }
            context.SaveChanges();
        }
    }
}
