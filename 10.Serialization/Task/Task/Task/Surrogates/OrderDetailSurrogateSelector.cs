using System;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Surrogates
{
    public class OrderDetailSurrogateSelector : ISurrogateSelector
    {
        private readonly Northwind _context;

        public OrderDetailSurrogateSelector(Northwind context)
        {
            _context = context;
        }

        public void ChainSelector(ISurrogateSelector selector)
        {
            throw new NotImplementedException();
        }

        public ISurrogateSelector GetNextSelector()
        {
            throw new NotImplementedException();
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            selector = this;

            if (type == typeof(Order_Detail))
            {
                return new OrderDetailSerializationSurrogate(_context);
            }

            selector = null;
            return null;
        }
    }
}
