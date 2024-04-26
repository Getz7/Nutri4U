using Data_Base;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Modelo;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;
using NuGet.Packaging.Signing;
using System.Configuration;
using System.Net;
using User = Modelo.User;

namespace MVCConAzure.cs
{
    public class UserCRUDFactory
    {
        private string EndpointUri;
        private string PrimaryKey;
        private string databaseId = "Nutri4U";
        private string containerId = "Usuarios";
        private DocumentClient client;

        public UserCRUDFactory()
        {
            EndpointUri = "https://componentesgrupo7.documents.azure.com:443/";
            PrimaryKey = "tCNBr5q4pJRqwvGXnZyRu6CZgpsbytw2h36rjHQUq3OSuafEhWJ1hYjgnTZFCbzTnLC7tcAebGoiACDbDapAhQ==";
            Init();
        }
        private void Init()
        {
            try
            {
                client = new DocumentClient(
                    new Uri(EndpointUri),
                    PrimaryKey
                    );
            }
            catch (DocumentClientException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Food>> RetrieveAll()
        {
            var SQL = "Select * from c";
            List<Food> foods = new List<Food>();
            IDocumentQuery<Food> query = client.CreateDocumentQuery<Food>(
                UriFactory.CreateDocumentCollectionUri(databaseId, containerId), SQL)
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                foods.AddRange(await query.ExecuteNextAsync<Food>());
            }
            return foods;
        }

        public Boolean Create(User user)
        {
            Boolean insertok = true;

            try
            {

                user.id = Guid.NewGuid().ToString("N");

                var database = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).Result.Resource;
                var collection = client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, containerId)).Result.Resource;

                var res = client.CreateDocumentAsync(collection.SelfLink, user).Result;

                if (!res.StatusCode.Equals(HttpStatusCode.Created))
                {
                    insertok = false;
                }
            }
            catch (DocumentClientException ex)
            {
                insertok = false;
                throw;
            }
            catch (Exception ex)
            {
                insertok = false;
                throw;
            }

            return insertok;
        }

        public async Task<User> LoginUser(string correo, string password)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(databaseId, containerId);
                var query = client.CreateDocumentQuery<User>(collectionUri,
                    new FeedOptions { MaxItemCount = -1 })
                    .Where(u => u.email == correo && u.password == password)
                    .AsDocumentQuery();

                while (query.HasMoreResults)
                {
                    var result = await query.ExecuteNextAsync<User>();
                    var user = result.FirstOrDefault();
                    if (user != null)
                        return user; // Return the entire user object
                }
            }
            catch (DocumentClientException ex)
            {
                // Handle DocumentClientException (e.g., log, throw, etc.)
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., log, throw, etc.)
                throw;
            }

            return null; // If user is not found
        }


        public async Task<User> GetUserById(string id)
        {
            try
            {
                if (id == null)
                {

                    return null;
                }


                id = id.Trim();
                Console.WriteLine($"Reading item with ID: {id}");

                // Query documents with a case-insensitive comparison
                var query = new SqlQuerySpec
                {
                    QueryText = "SELECT * FROM c WHERE LOWER(c.id) = @id",
                    Parameters = new SqlParameterCollection
            {
                new SqlParameter("@id", id.ToLowerInvariant())
            }
                };

                IDocumentQuery<User> documentQuery = client.CreateDocumentQuery<User>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, containerId),
                    query,
                    new FeedOptions { EnableCrossPartitionQuery = true })
                    .AsDocumentQuery();

                var user = new List<User>();
                while (documentQuery.HasMoreResults)
                {
                    user.AddRange(await documentQuery.ExecuteNextAsync<User>());
                }

                var foundUser = user.FirstOrDefault();
                if (foundUser == null)
                {
                    Console.WriteLine($"User not found for ID: {id}");
                }
                else
                {
                    Console.WriteLine($"User found: {JsonConvert.SerializeObject(foundUser)}");
                }

                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in read_item method: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateItem(User user)
        {
            try
            {
                // Convert the id to lowercase for consistency
                string lowerCaseId = user.id.ToLowerInvariant();
                Console.WriteLine($"Update action - Received id (lowercase): {lowerCaseId}");

                // Create document URI with the lowercase id
                Uri documentUri = UriFactory.CreateDocumentUri(databaseId, containerId, lowerCaseId);

                // Validate that the user exists before attempting to update
                var existingUser = await read_item(lowerCaseId);
                if (existingUser == null)
                {
                    Console.WriteLine($"User not found for ID: {lowerCaseId}");
                    return false;
                }

                // Perform the update
                var response = await client.ReplaceDocumentAsync(documentUri, user);

                Console.WriteLine($"Updated user with ID: {lowerCaseId}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n{ex.StackTrace}");

                if (ex is DocumentClientException documentClientException)
                {
                    Console.WriteLine($"DocumentClientException StatusCode: {documentClientException.StatusCode}");

                    // Add more details as needed
                }

                return false;
            }
        }
        public async Task<User> read_item(string id)
        {
            try
            {
                // Trim leading and trailing whitespaces
                id = id.Trim();
                Console.WriteLine($"Reading item with ID: {id}");

                // Query documents with a case-insensitive comparison
                var query = new SqlQuerySpec
                {
                    QueryText = "SELECT * FROM c WHERE LOWER(c.id) = @id",
                    Parameters = new SqlParameterCollection
            {
                new SqlParameter("@id", id.ToLowerInvariant())
            }
                };

                IDocumentQuery<User> documentQuery = client.CreateDocumentQuery<User>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, containerId),
                    query,
                    new FeedOptions { EnableCrossPartitionQuery = true })
                    .AsDocumentQuery();

                var users = new List<User>();
                while (documentQuery.HasMoreResults)
                {
                    users.AddRange(await documentQuery.ExecuteNextAsync<User>());
                }

                var foundUser = users.FirstOrDefault();
                if (foundUser == null)
                {
                    Console.WriteLine($"User not found for ID: {id}");
                }
                else
                {
                    Console.WriteLine($"User found: {JsonConvert.SerializeObject(foundUser)}");
                }

                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in read_item method: {ex.Message}");
                throw;
            }
        }


        public async Task<List<Food>> GetUserFoodById(string userId)
        {
            try
            {
                if (userId == null)
                {
                    return null;
                }
                else
                {
                    userId = userId.Trim();
                    var query = new SqlQuerySpec
                    {
                        QueryText = "SELECT c.idAlimento FROM AlimentoporUsuario c WHERE c.idUsuario = @userId",
                        Parameters = new SqlParameterCollection
                        {
                            new SqlParameter("@userId", userId)
                        }
                    };

                    List<string> foodIds = new List<string>();
                    IDocumentQuery<string> documentQuery = client.CreateDocumentQuery<string>(
                        UriFactory.CreateDocumentCollectionUri(databaseId, "AlimentoporUsuario"),
                        query,
                        new FeedOptions { EnableCrossPartitionQuery = true })
                        .AsDocumentQuery();

                    while (documentQuery.HasMoreResults)
                    {
                        var response = await documentQuery.ExecuteNextAsync();
                        foreach (var document in response)
                        {
                            JObject jsonObject = JObject.Parse(document.ToString());
                            // Assuming 'idAlimento' is the property holding the food IDs
                            string foodId = jsonObject["idAlimento"].ToString();
                            foodIds.Add(foodId);
                        }
                    }


                    List<Food> foods = new List<Food>();

                    foreach (string foodId in foodIds)
                    {
                        var foodQuery = new SqlQuerySpec
                        {
                            QueryText = "SELECT * FROM c WHERE LOWER(c.id) = @id",
                            Parameters = new SqlParameterCollection
                            {
                                new SqlParameter("@id", foodId)
                            }
                        };

                        IDocumentQuery<Food> foodDocumentQuery = client.CreateDocumentQuery<Food>(
                            UriFactory.CreateDocumentCollectionUri(databaseId, "Alimentos"),
                            foodQuery,
                            new FeedOptions { EnableCrossPartitionQuery = true })
                            .AsDocumentQuery();

                        while (foodDocumentQuery.HasMoreResults)
                        {
                            foods.AddRange(await foodDocumentQuery.ExecuteNextAsync<Food>());
                        }
                    }

                    return foods;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserFoodById method: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteUserFoodById(string userId, string foodId)
        {
            try
            {
                if (userId == null)
                {
                    return false;
                }
                else
                {
                    userId = userId.Trim();

                    var query = new SqlQuerySpec
                    {
                        QueryText = "SELECT c.id FROM AlimentoporUsuario c WHERE c.idUsuario = @userId AND c.idAlimento = @foodId",
                        Parameters = new SqlParameterCollection
                        {
                            new SqlParameter("@userId", userId),
                            new SqlParameter("@foodId", foodId)
                        }
                    };

                    List<string> foodIds = new List<string>();
                    IDocumentQuery<string> documentQuery = client.CreateDocumentQuery<string>(
                        UriFactory.CreateDocumentCollectionUri(databaseId, "AlimentoporUsuario"),
                        query,
                        new FeedOptions { EnableCrossPartitionQuery = true })
                        .AsDocumentQuery();

                    while (documentQuery.HasMoreResults)
                    {
                        var response = await documentQuery.ExecuteNextAsync();
                        foreach (var document in response)
                        {
                            JObject jsonObject = JObject.Parse(document.ToString());
                            string AlimentoUsuarioId = jsonObject["id"].ToString();
                            foodIds.Add(AlimentoUsuarioId);
                        }
                    }

                    foreach (string AlimentoUsuarioId in foodIds)
                    {
                        var deleteResult = await delete_UserFood(AlimentoUsuarioId);
                        if (!deleteResult)
                        {
                            return false; // Return false if deletion of any food fails
                        }
                    }

                    return true; // Return true if all foods were successfully deleted
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteUserFoodById method: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> delete_UserFood(string id)
        {
            bool deleteOk = true;

            try
            {
                var response = await client.DeleteDocumentAsync(
                    UriFactory.CreateDocumentUri(databaseId, "AlimentoporUsuario", id),
                    new RequestOptions { PartitionKey = new PartitionKey(id) });
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    deleteOk = false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                deleteOk = false;
                throw;
            }

            return deleteOk;
        }

        public Boolean CreateUserFood(UserFood food)
        {
            Boolean insertok = true;

            try
            {

                food.Id = Guid.NewGuid().ToString("N");

                var database = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).Result.Resource;
                var collection = client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, "AlimentoporUsuario")).Result.Resource;

                var res = client.CreateDocumentAsync(collection.SelfLink, food).Result;

                if (!res.StatusCode.Equals(HttpStatusCode.Created))
                {
                    insertok = false;
                }
            }
            catch (DocumentClientException ex)
            {
                insertok = false;
                throw;
            }
            catch (Exception ex)
            {
                insertok = false;
                throw;
            }

            return insertok;
        }



    }
}
