from pymongo import MongoClient

from settings import MONGO_USERNAME, MONGO_PASSWORD

client = MongoClient('localhost', 27017)
db = client.congress_report
users = db.users

class User:
    def __init__(self, username, email):
        self.username = username
        self.email = email
    
    def save(self):
        return users.insert({ "username": self.username, "email": self.email})
        # TODO: error handling
        # try:
        #     result = users.insert({ "username": self.username, "email": self.email)
        #     return result
        # except:
        #     return 
