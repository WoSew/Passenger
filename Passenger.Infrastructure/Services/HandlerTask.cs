using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private readonly IHandler _handler;
        private readonly Func<Task> _run;
        private Func<Task> _validate;
        private Func<Task> _always;
        private Func<Task> _onSuccess;
        private Func<Exception, Task> _onError;
        private Func<PassengerException, Task> _onCustomError;
        private bool _propagateException = true; 
        private bool _executeError = true; 

        public HandlerTask(IHandler handler, Func<Task> run, Func<Task> validate = null)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
        }
        
        public IHandlerTask Always(Func<Task> always)
        {
            _always = always;

            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;

            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        public IHandler Next()
            => _handler;

        public IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomError;
            _propagateException = propagateException;
            _executeError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false, bool executeOnError = false)
        {
            _onError = onError;
            _propagateException = propagateException;
            _executeError = executeOnError;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> OnSuccess)
        {
            _onSuccess = OnSuccess;
            
            return this;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                if(_validate != null)
                {
                    await _validate();
                }
                await _run();
                if(_onSuccess != null)
                {
                    await _onSuccess();
                }
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(exception);
                if(_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if(_always != null)
                {
                    await _always();
                }
            }         
        }

        private async Task HandleExceptionAsync(Exception exception)
        {
            var custromException = exception as PassengerException; 
            if(custromException != null)
            {
                if(_onCustomError != null)
                {
                    await _onCustomError(custromException);
                }
            }

            var executeOnError = _executeError || custromException == null;
            if(!executeOnError)
            {
                return;
            }
            if(_onError != null)
            {
                await _onError(exception);
            }
        }
    }
}