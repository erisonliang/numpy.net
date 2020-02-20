import unittest
import numpy as np

class Test_UFUNC_FLOAT_Tests(unittest.TestCase):

    #region UFUNC FLOAT Tests

    #region OUTER Tests
    def test_AddOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.add.outer(a1,a2)
        print(b)

    def test_SubtractOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.subtract.outer(a1,a2)
        print(b)

    def test_MultiplyOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.multiply.outer(a1,a2)
        print(b)

    def test_DivideOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.divide.outer(a1,a2)
        print(b)

    def test_RemainderOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.remainder.outer(a1,a2)
        print(b)

    def test_FModOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.fmod.outer(a1,a2)
        print(b)

    def test_SquareOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.square.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ReciprocalOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.reciprocal.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_OnesLikeOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.ones_like.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_SqrtOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
        
        try :
            b = np.sqrt.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_NegativeOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.negative.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

        
    def test_AbsoluteOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.absolute.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 

    def test_InvertOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.invert.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LeftShiftOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.left_shift.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_RightShiftOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.right_shift.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

            
    def test_BitwiseAndOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.bitwise_and.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

                        
    def test_BitwiseOrOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.bitwise_or.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_BitwiseXorOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        try :
            b = np.bitwise_xor.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LessOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.less.outer(a1,a2)
        print(b)

    def test_LessEqualOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.less_equal.outer(a1,a2)
        print(b)

    def test_EqualOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.equal.outer(a1,a2)
        print(b)

    def test_NotEqualOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.not_equal.outer(a1,a2)
        print(b)

    def test_GreaterOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.greater.outer(a1,a2)
        print(b)

    def test_GreaterEqualOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.greater_equal.outer(a1,a2)
        print(b)

    def test_FloorDivideOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.floor_divide.outer(a1,a2)
        print(b)

    def test_TrueDivideOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.true_divide.outer(a1,a2)
        print(b)

    def test_LogicalAndOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.logical_and.outer(a1,a2)
        print(b)

    def test_LogicalOrOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
  
        b = np.logical_or.outer(a1,a2)
        print(b)

    def test_FloorOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.floor.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_CeilOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.ceil.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_MaximumOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
 
        b = np.maximum.outer(a1,a2)
        print(b)

    def test_MinimumOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
 
        b = np.minimum.outer(a1,a2)
        print(b)
 
    def test_RintOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.rint.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ConjugateOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.conjugate.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_IsNANOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);

        try :
            b = np.isnan.outer(a1,a2)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_FMaxOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
 
        b = np.fmax.outer(a1,a2)
        print(b)

    def test_FMinOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
 
        b = np.fmin.outer(a1,a2)
        print(b)

    def test_HeavisideOuter_FLOAT(self):

        a1 = np.arange(0, 5, dtype=np.float32);
        a2 = np.arange(3, 8, dtype=np.float32);
 
        b = np.heaviside.outer(a1,a2)
        print(b)
        
      #endregion
  

    #region REDUCE Tests
    def test_AddReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
  
        b = np.add.reduce(a1)
        print(b)

    def test_SubtractReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10)); 
        
        b = np.subtract.reduce(a1)
        print(b)

    def test_MultiplyReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
  
        b = np.multiply.reduce(a1)
        print(b)

    def test_DivideReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.divide.reduce(a1)
        print(b)

    def test_RemainderReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        b = np.remainder.reduce(a1)
        print(b)

    def test_FModReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.fmod.reduce(a1)
        print(b)

    def test_SquareReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.square.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ReciprocalReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        try :
            b = np.reciprocal.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_OnesLikeReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        try :
            b = np.ones_like.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_SqrtReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));        

        try :
            b = np.sqrt.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_NegativeReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        try :
            b = np.negative.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

        
    def test_AbsoluteReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.absolute.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 

    def test_InvertReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        try :
            b = np.invert.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LeftShiftReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        try :
            b = np.left_shift.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_RightShiftReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));  

        try :
            b = np.right_shift.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

            
    def test_BitwiseAndReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        try :
            b = np.bitwise_and.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

                        
    def test_BitwiseOrReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        try :
            b = np.bitwise_or.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_BitwiseXorReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
  
        try :
            b = np.bitwise_xor.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LessReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.less.reduce(a1)
        print(b)

    def test_LessEqualReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.less_equal.reduce(a1)
        print(b)

    def test_EqualReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        b = np.equal.reduce(a1)
        print(b)

    def test_NotEqualReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.not_equal.reduce(a1)
        print(b)

    def test_GreaterReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.greater.reduce(a1)
        print(b)

    def test_GreaterEqualReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.greater_equal.reduce(a1)
        print(b)

    def test_FloorDivideReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.floor_divide.reduce(a1)
        print(b)

    def test_TrueDivideReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.true_divide.reduce(a1)
        print(b)

    def test_LogicalAndReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.logical_and.reduce(a1)
        print(b)

    def test_LogicalOrReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.logical_or.reduce(a1)
        print(b)

    def test_FloorReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.floor.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_CeilReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.ceil.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_MaximumReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.maximum.reduce(a1)
        print(b)

    def test_MinimumReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
 
        b = np.minimum.reduce(a1)
        print(b)
 
    def test_RintReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.rint.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ConjugateReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.conjugate.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_IsNANReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));

        try :
            b = np.isnan.reduce(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_FMaxReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.fmax.reduce(a1)
        print(b)

    def test_FMinReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
       
        b = np.fmin.reduce(a1)
        print(b)

    def test_HeavisideReduce_FLOAT(self):

        a1 = np.arange(0, 100, dtype=np.float32).reshape((10,10));
        
        b = np.heaviside.reduce(a1)
        print(b)
        
      #endregion
  
     
    #region ACCUMULATE Tests
    def test_AddAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        b = np.add.accumulate(a1)
        print(b)

    def test_SubtractAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
        
        b = np.subtract.accumulate(a1)
        print(b)

    def test_MultiplyAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        b = np.multiply.accumulate(a1)
        print(b)

    def test_DivideAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.divide.accumulate(a1)
        print(b)

    def test_RemainderAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        b = np.remainder.accumulate(a1)
        print(b)

    def test_FModAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmod.accumulate(a1)
        print(b)

    def test_SquareAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.square.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ReciprocalAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.reciprocal.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_OnesLikeAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.ones_like.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_SqrtAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.sqrt.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_NegativeAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.negative.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

        
    def test_AbsoluteAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.absolute.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 

    def test_InvertAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.invert.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LeftShiftAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.left_shift.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_RightShiftAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.right_shift.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

            
    def test_BitwiseAndAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.bitwise_and.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

                        
    def test_BitwiseOrAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.bitwise_or.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_BitwiseXorAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        try :
            b = np.bitwise_xor.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LessAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.less.accumulate(a1)
        print(b)

    def test_LessEqualAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.less_equal.accumulate(a1)
        print(b)

    def test_EqualAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        b = np.equal.accumulate(a1)
        print(b)

    def test_NotEqualAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.not_equal.accumulate(a1)
        print(b)

    def test_GreaterAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.greater.accumulate(a1)
        print(b)

    def test_GreaterEqualAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.greater_equal.accumulate(a1)
        print(b)

    def test_FloorDivideAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.floor_divide.accumulate(a1)
        print(b)

    def test_TrueDivideAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.true_divide.accumulate(a1)
        print(b)

    def test_LogicalAndAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.logical_and.accumulate(a1)
        print(b)

    def test_LogicalOrAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.logical_or.accumulate(a1)
        print(b)

    def test_FloorAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.floor.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_CeilAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.ceil.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_MaximumAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.maximum.accumulate(a1)
        print(b)

    def test_MinimumAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
 
        b = np.minimum.accumulate(a1)
        print(b)
 
    def test_RintAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.rint.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ConjugateAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.conjugate.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_IsNANAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.isnan.accumulate(a1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_FMaxAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmax.accumulate(a1)
        print(b)

    def test_FMinAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmin.accumulate(a1)
        print(b)

    def test_HeavisideAccumulate_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
        
        b = np.heaviside.accumulate(a1)
        print(b)
        
      #endregion
     
     
    #region REDUCEAT FLOAT Tests
    def test_AddReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        b = np.add.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_SubtractReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
        
        b = np.subtract.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_MultiplyReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        b = np.multiply.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_DivideReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.divide.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_RemainderReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        b = np.remainder.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_FModReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmod.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_SquareReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.square.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ReciprocalReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.reciprocal.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_OnesLikeReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.ones_like.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_SqrtReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.sqrt.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_NegativeReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.negative.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

        
    def test_AbsoluteReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.absolute.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 

    def test_InvertReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.invert.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LeftShiftReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.left_shift.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")


    def test_RightShiftReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.right_shift.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

            
    def test_BitwiseAndReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.bitwise_and.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

                        
    def test_BitwiseOrReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        try :
            b = np.bitwise_or.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_BitwiseXorReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
  
        try :
            b = np.bitwise_xor.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_LessReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.less.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_LessEqualReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.less_equal.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_EqualReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        b = np.equal.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_NotEqualReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.not_equal.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_GreaterReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.greater.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_GreaterEqualReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.greater_equal.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_FloorDivideReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.floor_divide.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_TrueDivideReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.true_divide.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_LogicalAndReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.logical_and.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_LogicalOrReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.logical_or.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_FloorReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.floor.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_CeilReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.ceil.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_MaximumReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.maximum.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_MinimumReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
 
        b = np.minimum.reduceat(a1, [0, 2], axis = 1)
        print(b)
 
    def test_RintReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.rint.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_ConjugateReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.conjugate.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")
 
    def test_IsNANReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));

        try :
            b = np.isnan.reduceat(a1, [0, 2], axis = 1)
            print(b)
            self.fail("should have thrown exception")
        except:
            print("Exception occured")

    def test_FMaxReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmax.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_FMinReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
       
        b = np.fmin.reduceat(a1, [0, 2], axis = 1)
        print(b)

    def test_HeavisideReduceAt_FLOAT(self):

        a1 = np.arange(0, 9, dtype=np.float32).reshape((3,3));
        
        b = np.heaviside.reduceat(a1, [0, 2], axis = 1)
        print(b)
        
      #endregion
     

      #endregion 

if __name__ == '__main__':
    unittest.main()
