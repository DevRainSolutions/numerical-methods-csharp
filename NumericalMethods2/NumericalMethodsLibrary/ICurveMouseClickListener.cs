#region Using directives

#endregion

namespace Interfaces
{
    /// A CurveMouseClickListener defines an interface to handle mouse click
    /// performed on a scatterplot and passed to a curve definition.
    /// @see Scatterplot
    /// @see CurveDefinition
    ///
    ///
    /// @author Didier H. Besset
    /// @translator edgar.sanchez@logicstudio.net
    public interface ICurveMouseClickListener
    {

        /// Processes the mouse clicks received from the scatterplot.
        /// If a mouse listener has been defined, each point is tested for mouse click.
        /// If the mouse click falls within the symbol size of a point, the index of
        /// that point is passed to the mouse listener, along with the defined parameter.
        /// @see #setMouseListener
        /// @param index index of the curve point on which the mouse was clicked.
        /// @param param the curve identifier.
        bool HandleMouseClick(int index, object param);
    }
}
