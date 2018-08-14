using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using AutoMapper;
using gwrsyncronizer.BLL;
using gwrsyncronizer.DataAccess.Context;
using gwrsyncronizer.DataAccess.DbModel;
using gwrsyncronizer.DataReader;
using gwrsyncronizer.Model;
using NLog;

namespace gwrsyncronizer
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            logger.Info("Run Import Data");

            InsertDataBulk();

            logger.Info("Done Import");
        }

        private static void InsertDataBulk()
        {
            var targetDirectoryList = new List<string>();
            //targetDirectoryList.Add(@"C:/temp/pdfs");
            

            foreach (var targetDirectory in targetDirectoryList)
            {
                var fileEntries = Directory.GetFiles(targetDirectory);
                var files = fileEntries.Length;

                for (var i = 0; i < fileEntries.Length; i += 1000)
                {
                    var endScope = i + 1000;
                    if (endScope >= fileEntries.Length) endScope = fileEntries.Length;

                    using (var context = new HousingDbContext())
                    {
                        for (var j = i; j < endScope; j++)
                        {
                            var dr = new ReadFileData();
                            var data = dr.ReadFilePdf(fileEntries[j]);

                            var apd = new AnalysePdfData();
                            var dataList = apd.ConvertTextToList(data);
                            logger.Info("{0}/{1} Analyse file {2}", j, files, fileEntries[j]);
                            var result = apd.AnalysePdf(dataList, data);

                            try
                            {
                                var configEgids = new MapperConfiguration(cfg =>
                                {
                                    cfg.CreateMap<Housing, Egids>()
                                        .ForMember(egids => egids.GebFlaeche,
                                            opts => opts.UseValue(
                                                (int)(long.TryParse(result.GebFlaeche, out var number) ? number : -1)))
                                        .ForMember(egids => egids.AnzGeschosse,
                                            opts => opts.UseValue(
                                                (int)(long.TryParse(result.GebFlaeche, out var number) ? number : -1)))
                                        .ForMember(egids => egids.CreatedUser,
                                            opts => opts.UseValue(Environment.UserName))
                                        .ForMember(egids => egids.CreatedAt, opts => opts.UseValue(DateTime.Now))
                                        .ForMember(egids => egids.ValidNow, opts => opts.UseValue(1))
                                        .ForMember(egids => egids.ValidFrom, opts => opts.UseValue(DateTime.Now))
                                        ;
                                });

                                var iMapper = configEgids.CreateMapper();

                                var destinationEgids = iMapper.Map<Housing, Egids>(result);


                                context.Egids.Add(destinationEgids);


                                foreach (var edid in result.HousingEdids)
                                {
                                    // Console.WriteLine("{0} {1} {2} {3} {4}", edid.Edid, edid.Strasse, edid.Eingangnummer, edid.Plz, edid.Ort);

                                    var configEdids = new MapperConfiguration(cfg =>
                                    {
                                        cfg.CreateMap<HousingEdid, Edids>()
                                            .ForMember(edids => edids.CreatedUser,
                                                opts => opts.UseValue(Environment.UserName))
                                            .ForMember(edids => edids.CreatedAt, opts => opts.UseValue(DateTime.Now))
                                            .ForMember(edids => edids.ValidNow, opts => opts.UseValue(1))
                                            .ForMember(edids => edids.ValidFrom, opts => opts.UseValue(DateTime.Now))
                                            ;
                                    });

                                    var iMapperEdids = configEdids.CreateMapper();

                                    var destinationEdids = iMapperEdids.Map<HousingEdid, Edids>(edid);

                                    context.Edids.Add(destinationEdids);

                                    foreach (var ewid in edid.HousingEgidEwids)
                                    {
                                        // Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", ewid.EgidEwid, ewid.Ewid, ewid.AdminNr, ewid.PhysNr, ewid.Stockwerk, ewid.Lage, ewid.WohnungStatus, ewid.Blob);
                                        var config = new MapperConfiguration(cfg =>
                                        {
                                            cfg.CreateMap<HousingEgidEwid, Ewids>()
                                                .ForMember(ewids => ewids.CreatedUser,
                                                    opts => opts.UseValue(Environment.UserName))
                                                .ForMember(ewids => ewids.CreatedAt,
                                                    opts => opts.UseValue(DateTime.Now))
                                                .ForMember(ewids => ewids.ValidNow, opts => opts.UseValue(1))
                                                .ForMember(ewids => ewids.ValidFrom,
                                                    opts => opts.UseValue(DateTime.Now))
                                                ;
                                        });

                                        var iMapperEwid = config.CreateMapper();

                                        var destinationEwid = iMapperEwid.Map<HousingEgidEwid, Ewids>(ewid);

                                        context.Ewids.Add(destinationEwid);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                logger.Fatal("Check file {0} Error Message {1} InnerMessage {2}", fileEntries[j],
                                    e.Message, e.InnerException);
                            }
                        }

                        logger.Info("Store data in DB");
                        var dateTime1 = DateTime.Now;

                        context.SaveChanges();

                        var dateTime2 = DateTime.Now;

                        var diff = dateTime1 - dateTime2;
                        logger.Info("Data stored DB {0}", diff);

                        GC.Collect();
                    }
                }
            }
        }
    }
}