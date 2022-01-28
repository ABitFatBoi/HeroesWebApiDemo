using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HeroesWebApiDemo.Dtos;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HeroesWebApiDemo.IntegrationTests;

public class HeroesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HeroesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetAll_WithoutAnyHeroes_ReturnsEmptyResponse()
    {
        //Arrange
        
        //Act
        var response = await _client.GetAsync("Heroes");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await JsonSerializer.DeserializeAsync<List<Hero>>(await response.Content.ReadAsStreamAsync())).Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAll_WithTestHeroes_ReturnsAllHeroes()
    {
        //Arrange
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
        var response = await _client.GetAsync("Heroes");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var returnedHeroes = new List<HeroResponseDto> { hero1, hero2 };
        
        var hero1Returned = returnedHeroes.Find(h => h.Name == "Hero 1 (Ranged Mage)");
        var hero2Returned = returnedHeroes.Find(h => h.Name == "Hero 2 (Ranged Healer)");

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
    public async Task CreateOneHero_WithoutAnyHeroes_ReturnsCreatedAtActionResponse
        (string name, bool isMelee, HeroType type)
    {
        //Arrange

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