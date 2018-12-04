var Food = /** @class */ (function () {
    function Food(name, calories) {
        this._name = name;
        this._calories = calories;
    }
    Object.defineProperty(Food.prototype, "Name", {
        get: function () {
            return this._name;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Food.prototype, "Calories", {
        get: function () {
            return this._calories;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Food.prototype, "Taste", {
        get: function () { return this._taste; },
        set: function (value) {
            this._taste = value;
        },
        enumerable: true,
        configurable: true
    });
    return Food;
}());
//# sourceMappingURL=food.js.map