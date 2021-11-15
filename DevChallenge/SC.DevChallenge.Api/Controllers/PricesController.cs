using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace SC.DevChallenge.Api.Controllers
{
    public class Result
    {
        public string date;
        public string Price;

        public Result(DateTime dateIn, double priceIn)
        {
            date = ("" + dateIn).Replace('.', '/');
            Price = "" + priceIn;
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        List<string> dataList = new List<string>();

        [HttpGet("average")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string Average(
            [FromQuery] string portfolio,
            [FromQuery] string owner,
            [FromQuery] string instrument,
            [FromQuery] DateTime dateTime)
        {
            
        using(var reader = new StreamReader("input//data.csv"))
            {
                while (!reader.EndOfStream) 
                    dataList.Add(reader.ReadLine());
            }

            dataList.RemoveAt(0);

            DateTime dateTime2 = dateTime.AddSeconds(10000);

            double sum = 0;
            int i = 0;

            string[] splitedLine;
            foreach(string line in dataList)
            {
                splitedLine = line.Split(',');
                splitedLine[0] = splitedLine[0].Replace(" ", String.Empty);
                splitedLine[1] = splitedLine[1].Replace(" ", String.Empty);
                splitedLine[2] = splitedLine[2].Replace(" ", String.Empty);
                if(splitedLine[0] == portfolio && splitedLine[1] == owner && splitedLine[2] == instrument && DateTime.Parse(splitedLine[3]) > dateTime && DateTime.Parse(splitedLine[3]) < dateTime2)
                {
                    sum += Convert.ToDouble(splitedLine[4].Replace('.', ','));
                    i++;
                    //result += line.Replace(',', ' ');
                }
            }

            Result res = new Result(dateTime, sum/i);

            return "{\n" + "\t\"date\": " + "\"" + res.date + "\"" + ",\n\t\"price\": \"" + res.Price.Replace(',', '.') + "\"" + "\n},";
        }
    }
}
