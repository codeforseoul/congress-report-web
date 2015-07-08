from flask import Flask, request, render_template
app = Flask(__name__)

@app.route("/")
def index():
  return render_template('index.html')

@app.route("/signup", methods=['POST'])
def signup():
  return "hello world!"


if __name__ == "__main__":
  app.run(debug = True)
