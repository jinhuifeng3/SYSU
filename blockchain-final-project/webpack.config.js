module.exports = {
    entry: "./app/app.js",
    output: {
        path: __dirname + '/app',
        filename: "bundle.js"
    },
    module: {
        rules: [
            { test: /\.css$/, loader: "style-loader!css-loader" }
        ]
    }
};