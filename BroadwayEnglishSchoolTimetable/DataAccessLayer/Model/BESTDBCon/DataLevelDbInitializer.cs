using System.Data.Entity;

namespace DataAccessLayer.Model.BESTDBCon
{
    public class DataLevelDbInitializer : CreateDatabaseIfNotExists<BESTDBModel>
    {
        //CreateDatabaseIfNotExists
        //DropCreateDatabaseAlways
        //DropCreateDatabaseIfModelChanges
        protected override void Seed(BESTDBModel context)
        {
            context.levelOfEnglishes.AddRange(new[]{ new levelOfEnglish { level = "Starter1" },
                                                                    new levelOfEnglish { level = "Starter2" },
                                                                    new levelOfEnglish { level = "Elementary1" },
                                                                    new levelOfEnglish { level = "Elementary2" },
                                                                    new levelOfEnglish { level = "Pre-intermediate1" },
                                                                    new levelOfEnglish { level = "Pre-intermediate2" },
                                                                    new levelOfEnglish { level = "Intermediate1" },
                                                                    new levelOfEnglish { level = "Intermediate2" },
                                                                    new levelOfEnglish { level = "Upper-intermediate1" },
                                                                    new levelOfEnglish { level = "Upper-interediate2" },
                                                                    new levelOfEnglish { level = "Advanced"}});
            context.causeOfDisruptions.AddRange(new[] { new causeOfDisruption { cause = "Отмена клиентом заранее" },
                                                        new causeOfDisruption { cause = "Отмена клиентом в день занятия" },
                                                        new causeOfDisruption { cause = "Отмена школой" }});
            context.daysOfTheWeeks.AddRange(new[] { new daysOfTheWeek { day = "Понедельник" },
                                                    new daysOfTheWeek { day = "Вторник" },
                                                    new daysOfTheWeek { day = "Среда" },
                                                    new daysOfTheWeek { day = "Четверг" },
                                                    new daysOfTheWeek { day = "Пятница" },
                                                    new daysOfTheWeek { day = "Суббота" },
                                                    new daysOfTheWeek { day = "Воскресенье" }});

            context.formOfEducations.AddRange(new[] { new formOfEducation { form = "Групповые занятия" },
                                                    new formOfEducation { form = "Индивидуальные занятия" },
                                                    new formOfEducation { form = "Групповые/Индивидуальные занятия" }});

            context.mainMails.Add(new mainMail { mail = "ijw53107@awsoo.com", password = "7D@@vEs6ot" });
            context.openingHours.Add(new openingHour { open = new System.TimeSpan(9, 0, 0), close = new System.TimeSpan(21, 0, 0) });

            context.SaveChanges();
            base.Seed(context);
        }

    }
}
