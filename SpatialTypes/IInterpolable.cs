namespace SpatialTypes
{
    public interface IInterpolable<T>
    {
        /// <summary>
        /// Implementing this means you can understand T as a continuous rather than discrete variable,
        /// so it is possible to be at any fraction between two instances of T.
        /// </summary>
        /// <param name="end">Where this instance is the start of the interpolation, end is the end.</param>
        /// <param name="fraction">Should never be outside the range 0 to 1. 
        /// Interpolate(end, 0) should always be equal to this.
        /// Interpolate(end, 1) should always be equal to end.
        /// </param>
        T Interpolate(T end, double fraction);
    }
}
