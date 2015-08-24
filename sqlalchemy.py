# #-*- coding: utf-8 -*-

# import MySQLdb
# # from flask import Flask, redirect, url_for, request, render_template
# # app = Flask(__name__)

from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, String

def test():
  username = 'hoony'
  email = 'thechunsik@gmail.com'

test()

# Base = declarative_base(__name__)


# @app.route("/")
# def index():
#   # 이메일 신청을 하기 위한 웹페이지
#   return render_template('index.html')

# @app.route("/signup", methods=['POST'])
# def signup():
#   email = request.form['email']
#   address = request.form['address']
#   lawmaker = test(address)


# class Signup(Base):
#    _tablename_='signup'
#    email = db.Column(string)
#    address = db.Column(string)
#    lawmaker = db.Column(string)

# def signup(email,address,lawmaker):
#   self.email = email
#   self.address = address
#   self.lawmaker = lawmaker

# def _repr_(signup):
#   return self.email + self.address + self.lawmaker
  
#   # Open database connection
#   db = MySQLdb.connect("localhost", "root", "", "test")
#     # prepare a cursor object using cursor() method
#   cursor = db.cursor()

#   # execute SQL query using execute() method.
#   cursor.execute("INSERT into test (name, email) values ('hi', '%s')" % email)

#   # Fetch a single row using fetchone() method.
#   db.commit()

#   # disconnect from server
#   db.close()

#   # print request.form['email']
#   # print request.form['address']
#   return redirect(url_for('index'))


# if __name__ == "__main__":
#   app.run(debug = True)


# def test(address):
#   return 'lawmaker'

# session.commit()