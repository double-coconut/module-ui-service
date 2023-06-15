using System;

namespace UIService.Runtime.Core
{
    public interface IViewData
    {
        public class WrongData<TI> : Exception where TI : IViewData
        {
            public WrongData(Type viewType) : base($"Wrong Data Initialized to {viewType}, should be {typeof(TI)}")
            {
            }
        }

        public class WrongData<TW, TI> : WrongData<TI> where TI : IViewData
        {
            public WrongData() : base(typeof(TW))
            {
            }
        }
    }
}