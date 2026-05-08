Hecho usando Unity 6000.4.4f1

Estoy usando la última versión de unity ya que es la más nueva y hay funciones que Unity va dando de baja en favor de hacer las cosas de una nueva manera.
El último commit del lab final lo hice como a la 1:30 am. Hay un commit que hice como 2 min antes de las fecha de entrega, las 00:00. Puedes corregir ese si no aceptas lo hecho más allá de las 00:00.
Cambios respecto a la práctica en la versión anterior:

Lab4: 
    Custom Controls: para hacer que la clase sea un custom contol, le pongo el atributo [UxmlElement] a la clase que quiero que lo sea y la hago una clase parcial. Para que salgan las variables en el ui builder, le pongo el atributo [UxmlAttribute] a los setters de las variables. En este caso, [UxmlAttribute("Hp")] public int Hp{get => hp;set{};}, le digo que el nombre que quiero que tenga en el editor es Hp. El valor por defecto se lo pongo a la variable que guarda internamente la hp.
