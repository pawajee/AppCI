Mixins for Multiple Inheritance-like Behavior
While JavaScript has no direct support for multiple inheritance, you can use mixins to simulate this feature. Mixins allow objects to draw from multiple sources, bringing together functionality from different classes.


Copy Code

let SerializableMixin = Base => class extends Base {
    serialize() {
        return JSON.stringify(this);
    }
};

let ActivatableMixin = Base => class extends Base {
    activate() {
        console.log('Activated');
    }
    deactivate() {
        console.log('Deactivated');
    }
};

class User {
    constructor(name) {
        this.name = name;
    }
}

class SuperUser extends SerializableMixin(ActivatableMixin(User)) {
    // SuperUser now has both serialization and activatable capabilities
}

let user = new SuperUser("Jane");
user.activate(); // From ActivatableMixin
console.log(user.serialize()); // From SerializableMixin
