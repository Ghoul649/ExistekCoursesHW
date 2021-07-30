using HW.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    partial class Program
    {
        static void InitCommands() 
        {
            #region help
            Command help = new Command()
            {
                KeyWord = "help",
                Description = "Display all commands"
            };
            commandManager.AddCommand(help);
            help.Action = (args) =>
            {
                foreach (var cmd in commandManager.Commands.OrderBy(c => c.KeyWord))
                {
                    Console.WriteLine(cmd.GetInfo());
                }
            };
            #endregion
            #region helpCommand
            Command helpCommand = new Command()
            {
                KeyWord = "helpCommand",
                Description = "Display info about specific command"
            };
            helpCommand.AddParam(
                new Parameter<string>() 
                {
                    Name = "Command",
                    Description = "Existing command to read about",
                    ParameterType = ParameterType.OneWord
                });
            commandManager.AddCommand(helpCommand);
            helpCommand.Action = (args) =>
            {
                string name = (string)args[0];
                var cmd = commandManager.Commands.Where(c => c.KeyWord == name).First();
                Console.WriteLine(cmd.GetFullInfo());
            };
            #endregion
            #region list
            Command list = new Command() 
            {
                KeyWord = "list",
                Description = "Display list of products"
            };
            commandManager.AddCommand(list);
            list.Action = (args) =>
            {
                foreach (Product p in products)
                {
                    Console.WriteLine(p);
                }
            };
            #endregion
            #region sell
            Command sell = new Command()
            {
                KeyWord = "sell",
                Description = "Sell several uints of specified product"
            };
            sell.AddParam(
                new Parameter<uint>()
                {
                    Name = "ProductId",
                    Description = "Product Id",
                    ParameterType = ParameterType.OneWord
                })
                .AddParam(
                new Parameter<int>()
                {
                    Name = "Count",
                    Description = "Count",
                    ParameterType = ParameterType.OneWord
                });
            commandManager.AddCommand(sell);
            sell.Action = (args) =>
            {
                var product = products.Where(prd => prd.ID == (uint)args[0]).First();
                int lcount = product.Count;
                product.Count -= (int)args[1];
                Console.WriteLine($"Sold {lcount - product.Count} units of{product.GetFullName()}");
            };
            #endregion
            #region add
            Command add = new Command()
            {
                KeyWord = "add",
                Description = "Add several units to specified product"
            };
            add.AddParam(
                new Parameter<uint>()
                {
                    Name = "ProductId",
                    Description = "Product Id",
                    ParameterType = ParameterType.OneWord
                })
                .AddParam(
                new Parameter<int>()
                {
                    Name = "Count",
                    Description = "Count",
                    ParameterType = ParameterType.OneWord
                });
            commandManager.AddCommand(add);
            add.Action = (args) =>
            {
                var product = products.Where(prd => prd.ID == (uint)args[0]).First();
                product.Count += (int)args[1];
                Console.WriteLine($"Added {args[1]} units of{product.GetFullName()}");
            };
            #endregion
           
        }
            //#region template
            //Command template = new Command()
            //{
            //    KeyWord = "template",
            //    Description = "template"
            //};
            //template.AddParam(
            //    new Parameter<string>()
            //    {
            //        Name = "template",
            //        Description = "template",
            //        ParameterType = ParameterType.OneWord
            //    });
            //commandManager.AddCommand(template);
            //template.Action = (args) =>
            //{
            //    throw new NotImplementedException();
            //};
            //#endregion
    }
}
