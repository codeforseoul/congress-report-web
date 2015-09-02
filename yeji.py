# -*- coding: utf-8 -*-

from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.orm import scoped_session, sessionmaker
from sqlalchemy.ext.declarative import declarative_base

engine = create_engine('sqlite:////tmp/test.db', convert_unicode = True)
db_session = scoped_session(sessionmaker(autocommit = False, autoflush = False, bind = engine))
Base = declarative_base()
Base.query = db_session.query_property()

class User(Base):
	__tablename__ = 'users'
	id = Column(Integer, primary_key=True)
	username = Column(String(50))
	email = Column(String(120))

	def __init__(self, username=None, email=None):
		self.username = username
		self.email = email

	def __repr__(self):
		return '사용자이름: %r, 사용자 이메일: %r' % (self.username, self.email)

def init_db():
	Base.metadata.create_all(bind=engine)

def shutdown_session(exception=None):
	db_session.remove()

def insert_user(username, email):
	u = User(username, email)
	db_session.add(u)
	db_session.commit()
	shutdown_session()

def get_users():
	init_db()
	return User.query.all()

if __name__ == "__main__":
	print('running test code...')
