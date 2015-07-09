from flask import Flask, request, render_template
app = Flask(__name__)

@app.route("/")
def index():
  # 이메일 신청을 하기 위한 웹페이지
  return render_template('index.html')

@app.route("/signup", methods=['POST'])
def signup():
  #
  # 예지님 코드(form 으로 넘어온 데이터를 디비에 저장)
  #
  return "hello world!"


if __name__ == "__main__":
  app.run(debug = True)
