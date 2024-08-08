class Alphabet:
    def __init__(self, value):
        self._value = value

    # getting the values
    def getValue(self,name):
        print('Getting value')
        return getattr(self, '_' + name);

    # setting the values
    def setValue(self,name, value):
        print('Setting value to ' + value)
        setattr(self, '_' + name, value)

    # deleting the values
    def delValue(self):
        print('Deleting value')
        del self._value
        
    def addProperty(self, name, value):
        fget = lambda self: self.getValue(name)
        fset = lambda self, value: self.setValue(name, value)
     
        setattr(self.__class__, name, property(fget, fset))
        # add corresponding local variable
        setattr(self, '_' + name, value)
        print('adding value')
        
ins=Alphabet("test")
ins.addProperty("testProp","TestValue")
print(ins.testProp)
