namespace Warehouse.Models
{
    using System;

    public abstract class ModelBase : IEquatable<ModelBase>
    {
        private readonly Guid guid = Guid.NewGuid();
        private long id;

        public virtual long Id
        {
            get { return id; }
        }

        public virtual Guid Guid
        {
            get { return guid; }
        }

        #region Implementation of IEquatable<ModelBase>
        /// <summary>
        ///     Equalses the specified model base.
        /// </summary>
        /// <param name="modelBase">The model base.</param>
        /// <returns></returns>
        public virtual bool Equals(ModelBase modelBase)
        {
            if (modelBase == null) return false;
            return Id == modelBase.Id && Equals(Guid, modelBase.Guid);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.
        /// </param>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        ///     The <paramref name="obj" /> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as ModelBase);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ModelBase x, ModelBase y)
        {
            return Equals(x, y);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ModelBase x, ModelBase y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = Guid.GetHashCode();
                result = (result * 397) ^ Id.GetHashCode();
                return result;
            }
        }
    }
    #endregion
}