﻿namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class Identifier<T>
    {
        protected Identifier(T value)
        {
            Value = value;
        }

        public virtual T Value { get; }
    }
}
