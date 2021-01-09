using System.Linq;
using TailorApp.Domain.Entities;

namespace TailorApp.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            Category[] category = new Category[]
            {
            new Category{Name="Shirt",Description="formal shirt"},
            new Category{Name="Pant",Description="formal pant"},
            new Category{Name="Suit",Description="full package "},
            new Category{Name="Dress",Description="Long"},
            new Category{Name="Top",Description="one piece"},

            };
            foreach (Category c in category)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            Measurement[] measurement = new Measurement[]
            {
            new Measurement{Name="Height",Description="cm"},
            new Measurement{Name="Length",Description="inch"},
            new Measurement{Name="Width",Description="inch"},
            new Measurement{Name="Sleve",Description="inch"},
            new Measurement{Name="Weist",Description="inch"}

            };
            foreach (Measurement m in measurement)
            {
                context.Measurements.Add(m);
            }
            context.SaveChanges();





            CategoryMeasurement[] enrollments = new CategoryMeasurement[]
            {
                new CategoryMeasurement {
                    CategoryID = category.Single(s => s.Name == "Shirt").CategoryID,
                    MeasurementID = measurement.Single(c => c.Name == "Height" ).MeasurementID,
                },
                   new CategoryMeasurement {
                    CategoryID = category.Single(s => s.Name == "Shirt").CategoryID,
                    MeasurementID = measurement.Single(c => c.Name == "Length" ).MeasurementID,
                },
                   new CategoryMeasurement {
                    CategoryID = category.Single(s => s.Name == "Pant").CategoryID,
                    MeasurementID = measurement.Single(c => c.Name == "Height" ).MeasurementID,
                },
                    new CategoryMeasurement {
                    CategoryID = category.Single(s => s.Name == "Pant").CategoryID,
                    MeasurementID = measurement.Single(c => c.Name == "Length" ).MeasurementID,
                },
                    new CategoryMeasurement {
                    CategoryID = category.Single(s => s.Name == "Pant").CategoryID,
                    MeasurementID = measurement.Single(c => c.Name == "Weist" ).MeasurementID,
                },

            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}


