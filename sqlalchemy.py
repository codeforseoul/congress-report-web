# #-*- coding: utf-8 -*-

# import MySQLdb
# # from flask import Flask, redirect, url_for, request, render_template
# # app = Flask(__name__)

from sqlalchemy import create_engine
from sqlalchemy.orm import scoped_session, sessionmaker
from sqlalchemy.ext.declarative import declarative_base

engine = create_engine('sqlite:////tmp/test.db', convert_unicode = True)
db_session = scoped_session(sessionmaker(autocommit = false, autoflush = false, bind = engine))
Base = declarative_base()
Base.query = db_session.query_property()

def init_db():
	import index.py.models 
	Base.metadata.create_all(bind=engine)

from index.py.database import db_session
def shutdown_session(exception=None):
	db_session.remove(index.py)

from sqlalchemy import Column, Integer, String
from index.py.database import Base

class Signup(Base):
   _tablename_='signup'
   id = Column(Integer, primary_key=True)
   username = Column(String(50), unique=True)
   email = Column(string(120), unique=True)

   def _init_(self, username=None, email=None):
   	self.username = username
   	self.email = email

   def _repr_(self):
   	return '<signup % r>' % (self.name)

from index.py.database import init_db
init_db()

from index.py.database import db_session
from index.py.models import User 
u = signup('admin', 'admin@localhost')
db_session.add(u)
db_session.commit()
  

def test():
  username = 'hoony'
  email = 'thechunsik@gmail.com' 



# if __name__ == "__main__":
#   app.run(debug = True)


# def test(address):
#   return 'lawmaker'

# session.commit()