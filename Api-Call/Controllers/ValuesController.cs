using Api_Call.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;



namespace Api_Call.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class FoodController : ApiController
    {
        [Route("food")]
        public IEnumerable<Food> Get()
        {
            using(orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                return dbContext.Foods.ToList();
            }
        }



        [Route("food/{id}")]
        [HttpGet] // phương thuc get
        public Food Get(int id)
        {
            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                return dbContext.Foods.FirstOrDefault(food => food.id == id);
            }
        }

        [Route("food")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Food food)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            using(orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                dbContext.Foods.Add(new Food()
                {
                    title=food.title,
                    price=food.price,
                    picture=food.picture,
                    category=food.category,
                });

                dbContext.SaveChanges();
            }
            return Ok("Post Success");

        }

        [Route("food")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] Food food)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                var existingFood = dbContext.Foods.Where(s => s.id == food.id)
                                                        .FirstOrDefault<Food>();

                if (existingFood != null)
                {
                    existingFood.title= food.title;
                    existingFood.price=food.price;
                    existingFood.picture = food.picture;
                    existingFood.category = food.category;

                    dbContext.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok("Updated success");
        }

        [Route("food/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid food id");

            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                var food = dbContext.Foods
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                dbContext.Entry(food).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }

            return Ok("Delete success");
        }


        [Route("category")]
        [HttpGet]
        public IEnumerable<category> GetCategory()
        {
            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                return dbContext.categories.ToList();
            }
        }

        [Route("category/{id}")]
        [HttpGet]
        public category GetCategory(int id)
        {
            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                return dbContext.categories.FirstOrDefault(e => e.id == id);
            }
        }

        [Route("category")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] category category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                dbContext.categories.Add(new category()
                {
                    title = category.title,
                    picture = category.picture,
                });

                dbContext.SaveChanges();
            }
            return Ok("Post Success");

        }

        [Route("category")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] category category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                var existingFood = dbContext.categories.Where(s => s.id == category.id)
                                                        .FirstOrDefault<category>();

                if (existingFood != null)
                {
                    existingFood.title = category.title;
                    existingFood.picture = category.picture;
                   

                    dbContext.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok("Updated success");
        }

        [Route("category/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid foodq id");

            using (orderFoodEntitiess dbContext = new orderFoodEntitiess())
            {
                var category = dbContext.categories
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                dbContext.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }

            return Ok("Delete success");
        }
    }
}
