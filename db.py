from pymongo import MongoClient

from settings import MONGO_USERNAME, MONGO_PASSWORD

client = MongoClient('localhost', 27017)
db = client.congress_report
users = db.users

class User:
    def __init__(self, username, email, assembly_id):
        self.username, self.email, self.assembly_id = username, email, assembly_id
    
    def save(self):
        print(self.assembly_id)
        return users.insert({'username': self.username, 'email': self.email, 'assembly_id': self.assembly_id})
        # TODO: error handling
        # try:
        #     result = users.insert({ "username": self.username, "email": self.email)
        #     return result
        # except:
        #     return 
