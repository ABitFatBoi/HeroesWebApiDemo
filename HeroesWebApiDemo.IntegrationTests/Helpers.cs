using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HeroesWebApiDemo.Dtos;
using Newtonsoft.Json;

namespace HeroesWebApiDemo.IntegrationTests;

public static class Helpers
{
    public static async Task<HeroResponseDto> CreateHeroRequestWithAssertionsAsync(this HttpClient client, HeroCreateDto heroCreateDto)
    {
        var response = await client.PostAsJsonAsync("Heroes", heroCreateDto);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var returnedHero = JsonConvert.DeserializeObject<HeroResponseDto>(await response.Content.ReadAsStringAsync());

        returnedHero.Should().NotBeNull($"Hero with the Name {heroCreateDto.Name}" +
                                        " hasn't returned as HeroResponseDto after creation.");
        returnedHero.Id.Should().NotBe(Guid.Empty);
        returnedHero.Type.Should().Be(heroCreateDto.Type);
        returnedHero.IsMelee.Should().Be(heroCreateDto.IsMelee);
        return returnedHero;
    }
}