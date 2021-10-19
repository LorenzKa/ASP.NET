﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApi.Services;

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService db;

        public PlayerController(PlayerService db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(db.GetPlayers());
        }
    }
}