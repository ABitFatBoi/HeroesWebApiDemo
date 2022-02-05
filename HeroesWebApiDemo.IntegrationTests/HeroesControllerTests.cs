using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Enums;
using HeroesWebApiDemo.Migrations;
using HeroesWebApiDemo.Routes.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace HeroesWebApiDemo.IntegrationTests;

public class HeroesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HeroesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(
            builder => builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("inMem");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureDeleted();
            })).CreateClient();
    }

    [Fact]
    public async Task GetAll_WithoutAnyHeroes_ReturnsEmptyResponse()
    {
        //Arrange
        await _client.AuthenticateAsync();
        
        //Act
        var response = await _client.GetAsync(ApiRoutes.Heroes.GetAll);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        JsonConvert.DeserializeObject<List<Hero>>(await response.Content.ReadAsStringAsync()).Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetById_WithoutAnyHeroes_ReturnsNotFoundResponse()
    {
        //Arrange
        await _client.AuthenticateAsync();
        
        //Act
        var response = await _client.GetAsync(
            ApiRoutes.Heroes.GetById.Replace("{id}", Guid.NewGuid().ToString()));
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        (await response.Content.ReadAsStringAsync()).Should().Be("\"Can't find hero with that id.\"");
    }

    [Fact]
    public async Task GetAll_WithTestHeroes_ReturnsAllHeroes()
    {
        //Arrange
        await _client.AuthenticateAsync();
        var hero1 = await _client.CreateHeroRequestWithAssertionsAsync(
                new HeroCreateDto {
                    Name = "Hero 1 (Ranged Mage)",
                    IsMelee = false,
                    Type = HeroType.Mage
                });
        var hero2 = await _client.CreateHeroRequestWithAssertionsAsync(
            new HeroCreateDto {
                Name = "Hero 2 (Ranged Healer)",
                IsMelee = false,
                Type = HeroType.Healer
            });
        
        //Act
        var response = await _client.GetAsync(ApiRoutes.Heroes.GetAll);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var returnedHeroes = JsonConvert.DeserializeObject<List<HeroResponseDto>>(
            await response.Content.ReadAsStringAsync());

        returnedHeroes.Should().NotBeNull();

        var hero1Returned = returnedHeroes.Find(h => 
            h.Name == "Hero 1 (Ranged Mage)");
        var hero2Returned = returnedHeroes.Find(h => 
            h.Name == "Hero 2 (Ranged Healer)");

        hero1Returned.Should().NotBeNull("Can't find hero with the Name \"Hero 1 (Ranged Mage)\"");
        hero2Returned.Should().NotBeNull("Can't find hero with the Name \"Hero 2 (Ranged Healer)\"");

        hero1Returned!.Type.Should().Be(hero1.Type);
        hero1Returned.IsMelee.Should().Be(hero1.IsMelee);
        
        hero2Returned!.Type.Should().Be(hero2.Type);
        hero2Returned.IsMelee.Should().Be(hero2.IsMelee);
    }
    
    [Theory]
    [InlineData("Hero 1 (Ranged Mage)", false, HeroType.Mage)]
    [InlineData("Hero 2 (Melee Healer)", true, HeroType.Healer)]
    public async Task Create_OneHeroWithoutAnyHeroes_ReturnsCreatedAtActionResponse
        (string name, bool isMelee, HeroType type)
    {
        //Arrange
        await _client.AuthenticateAsync();

        //Act
        await _client.CreateHeroRequestWithAssertionsAsync(
            new HeroCreateDto {
                Name = name,
                IsMelee = isMelee,
                Type = type
            });

        //Assert
        //Done in CreateHeroRequestWithAssertionsAsync
    }
}