using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TravelAppAPI.Models;

namespace TravelAppAPI.Sevices
{
    public class AuthServices
    {
        private readonly IMongoCollection<User> _users;
        public AuthServices(IOptions<TravelAppDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<User>(settings.Value.UsersCollectionName);
        }
        public async Task<List<User>> GetAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        public async Task<string> CheckExist(string userName, string password)
        {
            return (await _users.Find<User>(user => user.Username == userName && user.Password == CreateMD5(password)).FirstOrDefaultAsync())?.Id ?? String.Empty;
           
        }
        public async Task<bool> CheckExistUser(string username)
        {
            return await _users.Find<User>(user => user.Username == username).AnyAsync();
        }
        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
        public async Task<User> GetAsync(string id)
        {
            return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
        public async Task UpdateAsync(string id, User userIn)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, userIn);
        }
        public async Task RemoveAsync(User userIn)
        {
            await _users.DeleteOneAsync(user => user.Id == userIn.Id);
        }
        public async Task RemoveAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }
    }
}
