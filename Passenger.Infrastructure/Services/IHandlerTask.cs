using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services
{
    public interface IHandlerTask //fluent api
    {

         IHandlerTask Always(Func<Task> always);         //by zawsze moc wywoalc jakas operacje
         
         IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError,           //dla naszych wyjątków
         bool propagateException = false, bool executeOnError = false); //propagateException - by przekzać cały stack trace

        IHandlerTask OnError(Func<Exception, Task> onError,                         //dla wyjatków ale nie naszych
         bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnSuccess(Func<Task> OnSuccess);
        IHandlerTask PropagateException();  //on
        IHandlerTask DoNotPropagateException(); //off
        IHandler Next();
        Task ExecuteAsync();
    }
}