using System;

public class ShapeFactory {    
    public Shape getShape(String shapeType) {
        if (shapeType == null) {
            return null;
        }

        if (shapeType.Equals("CIRCLE")) {
            return new Circle();
        }

        if (shapeType.Equals("RECTANGLE"))
        {
            return new Rectangle();
        }

        if (shapeType.Equals("SQUARE"))
        {
            return new Square();
        }

        return null;
    }
}
