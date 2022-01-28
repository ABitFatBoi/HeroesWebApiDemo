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
    private readonly CustomWebApplicationFactory<Program> _factory;

    public HeroesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
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
        
        var hero1returned = returnedHeroes.Find(h => h.Name == "Hero 1 (Ranged Mage)");
        var hero2returned = returnedHeroes.Find(h => h.Name == "Hero 2 (Ranged Healer)");

        hero1returned.Should().NotBeNull("Can't find hero with the Name \"Hero 1 (Ranged Mage)\"");
        hero2returned.Should().NotBeNull("Can't find hero with the Name \"Hero 2 (Ranged Healer)\"");

        hero1returned.Type.Should().Be(hero1.Type);
        hero1returned.IsMelee.Should().Be(hero1.IsMelee);
        
        hero2returned.Type.Should().Be(hero2.Type);
        hero2returned.IsMelee.Should().Be(hero2.IsMelee);
    }
    
    [Fact]
    public async Task CreateOneHero_WithoutAnyHeroes_ReturnsCreatedAtActionResponse()
    {
        //Arrange

        //Act
        var hero = await _client.CreateHeroRequestWithAssertionsAsync(
            new HeroCreateDto {
                Name = "Hero 1 (Ranged Mage)",
                IsMelee = false,
                Type = HeroType.Mage
            });

        //Assert
        //Done in CreateHeroRequestWithAssertionsAsync
    }
}