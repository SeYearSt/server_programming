const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');
const User = require('./models/user');


const app = express();
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())
app.use(express.static(__dirname));
app.get('/', (_, res) => {
    res.sendFile(__dirname + '/index.html');
});

app.listen(process.env.PORT || 8080, () => console.log('Run server!'));


app.get('/getusers', async (_, res) => {
    const data = await User.find({});
    res.json(data);
});

app.post('/adduser', async (req, res) => {
    const user = new User(req.body);
    await user.save().catch(() => null);
    res.send('User added');
});

app.post('/deleteuser', async (req, res) => {
    await User.remove({ _id: req.body.id });
    res.send('User removed');
});

app.post('/updateuser', (req, res) => {
    User.findByIdAndUpdate(req.body._id, { $set: req.body }, err => {
        if (err) return next(err);
        res.send('User udpated');
    });
});
