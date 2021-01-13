
function toList(dict) {
    return Object.keys(dict).map(function (key) {
        return [key, dict[key]];
    });
}