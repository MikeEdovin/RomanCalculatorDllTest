using Microsoft.Extensions.DependencyInjection;
using SimpleRomanCalculator;
using SimpleRomanCalculator.Converter;
using SimpleRomanCalculator.Parser;
using System.ComponentModel.Design;
using Unity;

internal class Program
{
    private static void Main(string[] args)
    {
        var container = new UnityContainer();
        container.RegisterType<IRomanArabicConverter, RomanArabicConverter>();
        container.RegisterType<IInfixToPostfix, InfixToPostfix>();
        container.RegisterType<IPostfixToResult, PostfixToResult>();
        container.RegisterType<ICalculator,RomanCalculator>();
        var calculator=container.Resolve<ICalculator>();
        
        /*
        var services=ConfigureServices();
        var serviceProvider=services.BuildServiceProvider();
        var calculator=serviceProvider.GetRequiredService<ICalculator>();
        */
        IEnumerable<string> inputs = new List<string>() { "(MMMDCCXXIV - MMCCXXIX) * II", "(MMMMDCCXXIV - MMCCXXIX) * II", "MMM+MMM" };
        foreach (string input in inputs)
        {
            try
            {
                Console.WriteLine(calculator.Evaluate(input));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private static IServiceCollection ConfigureServices()
    {
       IServiceCollection services=new ServiceCollection();
        services.AddSingleton<IRomanArabicConverter,RomanArabicConverter>();
        services.AddSingleton<IInfixToPostfix,InfixToPostfix>();
        services.AddSingleton<IPostfixToResult,PostfixToResult>();
        services.AddSingleton<ICalculator,RomanCalculator>();
        return services;
    }



}