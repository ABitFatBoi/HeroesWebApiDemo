﻿using JetBrains.Annotations;

namespace HeroesWebApiDemo.Dtos;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class UserLoginDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}