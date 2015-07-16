#-*- coding: utf-8 -*-

from flask import Flask, redirect, url_for, request, render_template
app = Flask(__name__)

@app.route("/")
def index():
  # 이메일 신청을 하기 위한 웹페이지
  return render_template('index.html')

@app.route("/signup", methods=['POST'])
def signup():
  print request.form['email']
  print request.form['address']
  return redirect(url_for('index'))


if __name__ == "__main__":
  app.run(debug = True)
