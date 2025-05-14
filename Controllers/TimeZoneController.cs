using Microsoft.AspNetCore.Mvc;
using DateTimePro.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace DateTimePro.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TimeZoneController : ControllerBase
	{
		[HttpGet("local")]
		public IActionResult GetLocalTime()
		{
			var localTimeZone = System.TimeZoneInfo.Local;
			var localTime = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);
			return Ok(new TimeZoneInfoModel
			{
				Id = localTimeZone.Id,
				DisplayName = localTimeZone.DisplayName,
				StandardName = localTimeZone.StandardName,
				CurrentTime = localTime.ToString("yyyy-MM-dd HH:mm:ss")
			});
		}

		[HttpGet("all")]
		public IActionResult GetAllTimeZones()
		{
			var timeZones = System.TimeZoneInfo.GetSystemTimeZones().Select(tz => new TimeZoneInfoModel
			{
				Id = tz.Id,
				DisplayName = tz.DisplayName,
				StandardName = tz.StandardName,
				CurrentTime = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz).ToString("yyyy-MM-dd HH:mm:ss")
			}).ToList();
			return Ok(timeZones);
		}

		[HttpGet("{timeZoneId}")]
		public IActionResult GetTimeZoneById(string timeZoneId)
		{
			var timeZone = System.TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			if (timeZone == null)
			{
				return NotFound("Time zone not found.");
			}

			var currentTime = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
			return Ok(new TimeZoneInfoModel
			{
				Id = timeZone.Id,
				DisplayName = timeZone.DisplayName,
				StandardName = timeZone.StandardName,
				CurrentTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss")
			});
		}
	}
}