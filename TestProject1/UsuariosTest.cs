using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioAPI_DotNet8.Controllers;
using UsuarioAPI_DotNet8.Data;
using UsuarioAPI_DotNet8.Entities;
using Xunit;

namespace TestProject1
{
    public class UsuariosTest
    {




        public class UsuarioControllerTests
        {
            /*
             test 1 get
             */
            [Fact]
            public async Task GetAllNameUser_Returns_OkResult_With_Correct_User()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UsuariosDb")
                    .Options;

                using (var context = new DataContext(options))
                {
                    var controller = new UsuarioController(context);

                    // Adding test data to the in-memory database
                    context.Usuarios.Add(new Usuario { Id = 1, Name = "Amazing spider-man" });
                    await context.SaveChangesAsync();

                    // Act
                    var result = await controller.GetAllNameUser(1);

                    // Assert
                    var okResult = Assert.IsType<OkObjectResult>(result.Result);
                    var user = Assert.IsType<Usuario>(okResult.Value);
                    Assert.Equal("Amazing spider-man", user.Name);
                }
            }

            /*
             test 1 update
             */

            [Fact]
            public async Task UpdateUser_Returns_OkResult_After_Successful_Update()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UsuariosDb")
                    .Options;

                using (var context = new DataContext(options))
                {
                    var controller = new UsuarioController(context);

                    // Agregar un usuario de prueba a la base de datos en memoria
                    var initialUser = new Usuario { Id = 4, Name = "Spider-man", FirstName = "Peter", LastName = "Parker", Place = "New York" };
                    context.Usuarios.Add(initialUser);
                    await context.SaveChangesAsync();

                    // Crear un objeto Usuario con los datos actualizados
                    var updateUser = new Usuario
                    {
                        Id = 4,
                        Name = "Updated Spider-man",
                        FirstName = "Updated Peter",
                        LastName = "Updated Parker",
                        Place = "Updated New York"
                    };

                    // Act
                    var result = await controller.UpdateUser(updateUser);

                    // Assert
                    var okResult = Assert.IsType<OkObjectResult>(result.Result);
                    var updatedUsers = Assert.IsType<List<Usuario>>(okResult.Value);

                    // Verificar que la lista de usuarios actualizada contenga al usuario actualizado
                    Assert.Contains(updatedUsers, u => u.Id == updateUser.Id && u.Name == updateUser.Name && u.FirstName == updateUser.FirstName &&
                                                        u.LastName == updateUser.LastName && u.Place == updateUser.Place);
                }
            }


            /*
             test 3  delete
             */

            [Fact]
            public async Task DeleteUser_Returns_OkResult_After_Successful_Deletion()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UsuariosDb")
                    .Options;

                using (var context = new DataContext(options))
                {
                    var controller = new UsuarioController(context);

                    // Agregar un usuario de prueba a la base de datos en memoria
                    var userToDelete = new Usuario { Id = 1, Name = "Spider-man" };
                    context.Usuarios.Add(userToDelete);
                    await context.SaveChangesAsync();

                    // Act
                    var result = await controller.DeleteUser(1);

                    // Assert
                    var okResult = Assert.IsType<OkObjectResult>(result.Result);
                    var remainingUsers = Assert.IsType<List<Usuario>>(okResult.Value);

                    // Verificar que la lista de usuarios ahora no contiene al usuario borrado
                    Assert.DoesNotContain(remainingUsers, u => u.Id == userToDelete.Id);
                }
            }


            /*test 4 add*/
            [Fact]
            public async Task AddUser_Returns_OkResult_After_Successful_Addition()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UsuariosDb")
                    .Options;

                using (var context = new DataContext(options))
                {
                    var controller = new UsuarioController(context);

                    // Crear un nuevo usuario para agregar
                    var newUser = new Usuario
                    {
                        Id = 5,
                        Name = "Spider-man",
                        FirstName = "Peter",
                        LastName = "Parker",
                        Place = "New York"
                    };

                    // Act
                    var result = await controller.AddUser(newUser);

                    // Assert
                    var okResult = Assert.IsType<OkObjectResult>(result.Result);
                    var addedUsers = Assert.IsType<List<Usuario>>(okResult.Value);

                    // Verificar que la lista de usuarios ahora contiene al nuevo usuario agregado
                    Assert.Contains(addedUsers, u => u.Id == newUser.Id && u.Name == newUser.Name &&
                                                      u.FirstName == newUser.FirstName && u.LastName == newUser.LastName &&
                                                      u.Place == newUser.Place);
                }
            }

        }
    }
}
