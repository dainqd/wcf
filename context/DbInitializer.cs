using baseNetApi.models;
using Microsoft.EntityFrameworkCore;

namespace baseNetApi.context;

public class DbInitializer
{
    private readonly ModelBuilder modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }
    
    public void Seed()
    {
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                id = 1, role = Role.Admin, username = "admin",email = "admin@gmail.com",
                firstName = "supper", lastName = "admin",
                phoneNumber = "0989889889", address = "Hai Phong",
                birthday = "10-02-2003", gender = "Male", status = UserStatus.ACTIVE,
                password = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User(){
                id = 2, role = Role.User, username = "user",email = "user@gmail.com",
                firstName = "User", lastName = "New",
                phoneNumber = "0968886868", address = "Ha Noi",
                birthday = "01-01-2003", gender = "Male", status = UserStatus.ACTIVE,
                password = BCrypt.Net.BCrypt.HashPassword("123456")
                
            }
        );
        modelBuilder.Entity<Groups>().HasData(
            new Groups()
            {
                id = 1, group_name = "Home"
            },
            new Groups()
            {
                id = 2, group_name = "Work"
            },
            new Groups()
            {
                id = 3, group_name = "Company"
            }
        );
        modelBuilder.Entity<Contacts>().HasData(
            new Contacts()
            {
                id = 1, group_id = 1, name = "My father",number = "0898889889",description = "Apple", 
                avt = "https://ichef.bbci.co.uk/news/640/cpsprodpb/12A57/production/_109657367_a267a209-f3a5-4a8c-bf2b-5c80660d4fe0.jpg",
                status = ContactStatus.ACTIVE
            },
            new Contacts(){
                id = 2, group_id = 1, name = "My mother",number = "0986868888",description = "Apple", 
                avt = "https://ichef.bbci.co.uk/news/640/cpsprodpb/12A57/production/_109657367_a267a209-f3a5-4a8c-bf2b-5c80660d4fe0.jpg",
                status = ContactStatus.ACTIVE
            }
        );
    }
}