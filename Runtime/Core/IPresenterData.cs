using System;

namespace UIService.Runtime.Core
{
    public interface IPresenterData
    {
        public class WrongData<TI> : Exception where TI : IPresenterData
        {
            public WrongData(Type presenterType) : base($"Wrong Data Initialized to {presenterType}, should be {typeof(TI)}")
            {
            }
        }

        public class WrongData<TW, TI> : WrongData<TI> where TI : IPresenterData
        {
            public WrongData() : base(typeof(TW))
            {
            }
        }
    }
}