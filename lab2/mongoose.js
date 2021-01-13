const mongoose = require("mongoose");
mongoose.Promise = global.Promise;

mongoose.connect(
  "mongodb+srv://root:cool@cluster0.ubtym.mongodb.net/<dbname>?retryWrites=true&w=majority",
  { useNewUrlParser: true, useUnifiedTopology: true, useCreateIndex: true }
);

mongoose.set("useFindAndModify", false);
console.log("mongodb connect...");

module.exports = mongoose;
