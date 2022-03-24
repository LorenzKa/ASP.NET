﻿using Microsoft.AspNetCore.Mvc;
using SignalRStocksBackend.DTOs;
using SignalRStocksBackend.Entities;

namespace SignalRStocksBackend.Services;

public class StockService
{
    private readonly StockContext db;

    public StockService(StockContext db)
    {
        this.db = db;
    }

    public List<ShareDto> GetShares()
    {
        Console.WriteLine("here");
        return db.Shares.Select(x => new ShareDto().CopyPropertiesFrom(x)).ToList();
    }

    public UserDto GetUser(string name)
    {
        var user =  db.Users.FirstOrDefault(x => x.Name == name);
        if(user != null)
        {
            var userDto = new UserDto().CopyPropertiesFrom(user);
            userDto.Depots = db.UserShares.Where(x => x.User.Id == user.Id).ToList().Select(x => new DepotDto()
            {
                Amount = x.Amount,
                ShareName = x.Share.Name
            }).ToList();
            return userDto;
        }
        db.Users.Add(new User
        {
            Name = name,
            Cash = new Random().Next(1000, 10000),
            Id = db.Users.Count()
        });
        db.SaveChanges();
        return new UserDto().CopyPropertiesFrom(db.Users.First(x => x.Name == name));
    }
}